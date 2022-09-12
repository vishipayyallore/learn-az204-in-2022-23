using College.Core.Entities;

namespace College.Core.Interfaces
{

    public interface IProfessorsCosmosBll
    {
        Task<Professor> AddProfessor(Professor professor);

        Task<Professor> GetProfessorById(Guid professorId);

        Task<IEnumerable<Professor>> GetAllProfessors();

        Task<Professor> UpdateProfessor(Professor professor);

        Task<bool> DeleteProfessorById(Guid professorId);
    }

}