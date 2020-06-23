import { Routes } from '@angular/router';
import { HomeComponent, CounterComponent, LoginComponent } from "./components"
import { AuthGuard } from "./classes"

export const routs: Routes = [
  { path: '', component: HomeComponent, canActivate: [AuthGuard] },
  { path: 'counter', component: CounterComponent },
  { path: 'login', component: LoginComponent },
  { path: '**', redirectTo: '' }
];
