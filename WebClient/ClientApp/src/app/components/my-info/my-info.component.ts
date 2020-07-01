import {Component, OnInit} from '@angular/core'
import { EmployeeService } from "../../services";
import { EmployeeInfo } from "../../models";

@Component({
  templateUrl: './my-info.component.html',
})

export class MyInfoComponent implements OnInit {
  employee: EmployeeInfo = new EmployeeInfo();

  constructor(private empService: EmployeeService) { }

  ngOnInit(): void {
    this.empService.getCurrentEmployee().subscribe(emp => {
      this.employee = emp;
    });
  }

}
