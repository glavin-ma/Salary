import { Injectable, Inject } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Employee } from "../models";
import { AUTH_API_URL } from "../app.injection.tokens";
import { Observable } from "rxjs";

@Injectable({ providedIn: 'root' })
export class EmployeeService {

  constructor(private http: HttpClient, @Inject(AUTH_API_URL) private apiUrl) { }

  getEmployeesSalary(date: Date): Observable<Employee[]> {
    return this.http.get<Employee[]>(`${this.apiUrl}api/employee/calculate`);
  }
}
