import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { AuthService, MessageService } from "../services";

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {

  constructor(private router: Router, private auth: AuthService, private messageService: MessageService) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    if (this.auth.isAuthenticated()) {

      if (route.data.hasOwnProperty("roles")) {
        let roles: string[] = route.data["roles"];
        let user = this.auth.getUser();
        let filtered = roles.filter((role) => {
          return user.roles.includes(role);
        });
        if (filtered.length === 0) {
          this.messageService.showError("Forbidden Access");
          this.router.navigate(['/']);
        }
      }

      return true;
    }

    this.router.navigate(['/login'], { queryParams: { returnUrl: state.url } });
    return false;
  }
}
