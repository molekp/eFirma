using System.Collections.Generic;

namespace BussinessLogic.DTOs.EStore.EStoreCategoryDto
{
    public class EStoreCategoryManageDto
    {
        public int IdEStore { get; set; }

        public IEnumerable<EStoreCategoryDto> CategoryOfEStore { get; set; }
    }
}