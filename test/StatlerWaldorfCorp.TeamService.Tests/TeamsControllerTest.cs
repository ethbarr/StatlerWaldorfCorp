using Xunit;
using StatlerWaldorfCorp.TeamService.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using StatlerWaldorfCorp.TeamService.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace StatlerWaldorfCorp.TeamService
{
    public class TeamsControllerTest
    {

        [Fact]
        public void QueryTeamListReturnsCorrectTeams()
        {
            ITeamRepository repo = new MemoryTeamRepository();
            repo.AddTeam(new Team("one", Guid.NewGuid()));
            repo.AddTeam(new Team("two", Guid.NewGuid()));
            TeamsController controller = new TeamsController(new MemoryTeamRepository());
            var rawTeams = (IEnumerable<Team>)(controller.GetAllTeams() as ObjectResult).Value;
            List<Team> teams = new List<Team>(rawTeams);
            Assert.Equal(2, teams.Count);
            Assert.Equal("one", teams[0].Name);
            Assert.Equal("two", teams[1].Name);  
        }

        [Fact]
        public void CreateTeamAddsTeamToList() 
        {
            TeamsController controller = new TeamsController(new MemoryTeamRepository());
            
            var teams = (IEnumerable<Team>)
                (controller.GetAllTeams() as ObjectResult).Value;
            
            List<Team> original = new List<Team>(teams);
            
            Team t = new Team("sample");
            var result = controller.CreateTeam(t);

            var newTeamsRaw = 
                (IEnumerable<Team>)
                    (controller.GetAllTeams() as ObjectResult).Value;
    
            List<Team> newTeams = new List<Team>(newTeamsRaw);
            Assert.Equal(original.Count+1, newTeams.Count);
    
            var sampleTeam = newTeams.FirstOrDefault( 
                target => target.Name == "sample");
            Assert.NotNull(sampleTeam);            
        } 
 
    }
}
