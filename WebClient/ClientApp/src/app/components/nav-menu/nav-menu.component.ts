import { Component, OnInit } from '@angular/core';
import { AuthService } from "../../services/auth.service";

@Component({
  selector: 'app-nav-menu',
  templateUrl: 'nav-menu.component.html',
  styleUrls: ['nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {

  constructor(private auth: AuthService) { }

  roles: string[];

  ngOnInit(): void {
    this.initRoles();
    this.auth.getLoginDispatcher().subscribe(() => {
      this.initRoles();
    });
  }
  initRoles(): void {
    let user = this.auth.getUser();
    this.roles = user != undefined ? user.roles : undefined;
  }

  available(): boolean {
    let availableRoles: string[] = ["Manager", "Salesman"];
    if (this.roles == undefined) return false;
    let filtered = this.roles.filter((role) => {
      return availableRoles.includes(role);
    });
    return filtered.length !== 0;
  }

  isExpanded = false;

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
