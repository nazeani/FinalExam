using Business.Exceptions;
using Business.Extensions;
using Business.Services.Abstracts;
using Core.Models;
using Core.RepositoryAbstracts;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concrates
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IWebHostEnvironment _env;
        public TeamService(ITeamRepository teamRepository, IWebHostEnvironment env )
        {
            _teamRepository = teamRepository;
            _env = env;
        }

        public void Add(Team team)
        {
            if (team == null) throw new EntityNotFoundException("Team tapilmadi!!");
            team.ImageUrl=Helper.SaveFile(_env.WebRootPath,@"uploads\teams", team.ImageFile);
            _teamRepository.Add(team);
            _teamRepository.Commit();
        }

        public void Delete(int id)
        {
         var existTeam= _teamRepository.Get(x=>x.Id==id);
            if(existTeam == null) throw new EntityNotFoundException("Team tapilmadi!!");
            Helper.DeleteFile(_env.WebRootPath, @"uploads\teams", existTeam.ImageUrl);
            _teamRepository.Delete(existTeam);
            _teamRepository.Commit();
          
        }

        public List<Team> GetAllTeams(Func<Team, bool>? func = null)
        {
           return _teamRepository.GetAll(func);
        }

        public Team GetTeam(Func<Team, bool>? func = null)
        {
            return _teamRepository.Get(func);
        }

        public void Update(int id, Team team)
        {
            var oldTeam = _teamRepository.Get(x => x.Id == id);
            if (oldTeam == null) throw new EntityNotFoundException("Team tapilmadi!!");
            if(team.ImageFile != null)
            {
                Helper.DeleteFile(_env.WebRootPath, @"uploads\teams", oldTeam.ImageUrl);
                oldTeam.ImageUrl = Helper.SaveFile(_env.WebRootPath, @"uploads\teams", team.ImageFile);
            }
            oldTeam.Title = team.Title;
            oldTeam.Description = team.Description;
            oldTeam.XLink = team.XLink;
            oldTeam.FullName = team.FullName;
            oldTeam.FbLink = team.FbLink;
            oldTeam.InLink = team.InLink;
            oldTeam.IgLink = team.IgLink;
            _teamRepository.Commit();
        }
    }
}
