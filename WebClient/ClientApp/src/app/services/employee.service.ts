import { Injectable, Inject } from "@angular/core";
import { HttpClient, HttpParams } from '@angular/common/http';
import { Employee, EmployeeInfo } from "../models";
import { AUTH_API_URL } from "../app.injection.tokens";
import { Observable} from "rxjs";
import { DateFormatPipe } from "../pipes/date-format-pipe";

@Injectable({ providedIn: 'root' })
export class EmployeeService {

  constructor(private http: HttpClient, @Inject(AUTH_API_URL) private apiUrl, private dateFormat: DateFormatPipe) { }

  calculateSalaries(date: Date): Observable<Employee[]> {
    return this.http.get<Employee[]>(`${this.apiUrl}api/employee/calculate`, { params: new HttpParams().set('date', this.dateFormat.transform(date)) });
  }

  getCurrentEmployee(): Observable<EmployeeInfo> {
    return this.http.get<EmployeeInfo>(`${this.apiUrl}api/employee`);
  }

  getEmployee(id: string): Observable<EmployeeInfo> {
    return this.http.get<EmployeeInfo>(`${this.apiUrl}api/employee`, { params: new HttpParams().set('id', id) });
  }
}
