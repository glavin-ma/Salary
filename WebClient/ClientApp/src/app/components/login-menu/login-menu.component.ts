import { Component, OnInit } from '@angular/core'
import { User } from "../../models";
import { AuthService } from "../../services";

@Component({
  selector: 'login-menu',
  templateUrl: 'login-menu.component.html'
})
export class LoginMenuComponent implements OnInit {
  user: User;
  userName: string;
  constructor(private auth: AuthService) {

  }
  ngOnInit() {
    this.getUserInfo();
    this.auth.getLoginDispatcher().subscribe(() => {
      this.getUserInfo();
    });
  }


  isAuthenticated(): boolean { return this.auth.isAuthenticated() };

  logout() { this.auth.logout(); }

  private getUserInfo() {
    this.user = this.auth.getUser();
    this.userName = this.user == null ? '' : this.user.userName;
  }
}
