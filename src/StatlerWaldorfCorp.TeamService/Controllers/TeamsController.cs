using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using StatlerWaldorfCorp.TeamService.Models;
using System.Threading.Tasks;
using StatlerWaldorfCorp.TeamService.Persistence;

namespace StatlerWaldorfCorp.TeamService
{
  public class TeamsController : Controller
  {  
    ITeamRepository repository;
    public TeamsController(ITeamRepository repo) 
    {
      repository = repo;
    }

    [HttpGet]
    public virtual IActionResult GetAllTeams()
    {                  
      return this.Ok(repository.GetTeams());            
    }

    public virtual IActionResult CreateTeam(Team t)
    {
      repository.AddTeam(t);
			
			return this.Created($"/teams/{t.ID}", t);
    }
  }
}