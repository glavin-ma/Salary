using System;
using System.Collections.Generic;

namespace DTO.Employment
{
    public class EmployeeInfoDto : EmployeeDto
    {

        public string UserName { get; set; }
        public string Type { get; set; }
        public double BasicRate { get; set; }
        public DateTime EmploymentDate { get; set; }

        public IEnumerable<EmployeeDto> Dependants { get; set; }
    }
}
