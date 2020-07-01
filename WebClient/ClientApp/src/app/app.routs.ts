import { Routes } from '@angular/router';
import {  LoginComponent, MyInfoComponent, SalariesComponent } from "./components"
import { AuthGuard } from "./classes"

export const routs: Routes = [
  { path: '', component: MyInfoComponent, canActivate: [AuthGuard] },
  { path: 'my-info', component: MyInfoComponent, canActivate: [AuthGuard] },
  { path: 'salaries', component: SalariesComponent, canActivate: [AuthGuard], data: { roles:["Manager", "Salesman"]}  },
  { path: 'login', component: LoginComponent },
  { path: '**', redirectTo: '' }
];
