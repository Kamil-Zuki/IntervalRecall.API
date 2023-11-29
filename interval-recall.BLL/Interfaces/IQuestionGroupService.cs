using interval_recall.BLL.DTOs;
using interval_recall.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace interval_recall.BLL.Interfaces
{
    public interface IQuestionGroupService
    {
        Task CreateAsync(InQuestionGroupDTO questionGroupDTO);
        Task<List<OutQuestionGroupDTO>> GetAllAsync();

    }
}
