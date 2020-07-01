import { Component, OnInit } from '@angular/core'
import { EmployeeService } from "../../services";
import { Employee } from "../../models";

@Component({
  templateUrl: './salaries.component.html',
})

export class SalariesComponent implements OnInit {
  constructor(private empService: EmployeeService) { }
  employees: Employee[];
  selectedDate: Date;

  ngOnInit(): void {

  }
  getTotal(employees): number {
    var mapped = employees.map((item) => item.salary);
    return mapped.reduce((sum, curr) => sum + curr, 0);
  }
  calculate() {
    this.empService.calculateSalaries(this.selectedDate).subscribe((result) => {
      this.employees = result;
    });
  }

}
