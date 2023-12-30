using JamesRecipes.Models;
using JamesRecipes.Repository.Admin;
using JamesRecipes.Repository.FE;

namespace JamesRecipes.Service.Admin
{
    public class ContestEntryManagementService: IContestEntryManagementRepository
    {
        private readonly IContestEntryManagementRepository _contestEntryManagementRepository;
        
      

        public ContestEntryManagementService(IContestEntryManagementRepository contestEntryManagementRepository)
        {
            _contestEntryManagementRepository = contestEntryManagementRepository;
            
        }

        public List<ContestEntry> GetAllContestEntries()
        {
            return _contestEntryManagementRepository.GetAllContestEntries();
        }

        public ContestEntry GetContestEntry(int id)
        {
            return _contestEntryManagementRepository.GetContestEntry(id);
        }

       
    }
}
