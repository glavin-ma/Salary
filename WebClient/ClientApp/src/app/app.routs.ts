import { Routes } from '@angular/router';
import { HomeComponent,  LoginComponent } from "./components"
import { AuthGuard } from "./classes"

export const routs: Routes = [
  { path: '', component: HomeComponent, canActivate: [AuthGuard] },
  { path: 'login', component: LoginComponent },
  { path: '**', redirectTo: '' }
];
