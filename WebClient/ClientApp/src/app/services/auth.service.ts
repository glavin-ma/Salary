import { Injectable, Inject, EventEmitter } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Router } from '@angular/router';

import { tap } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { Token } from "../models";
import { AUTH_API_URL } from "../app.injection.tokens";
import { User } from "../models";

export const ACCESS_TOKEN_KEY = "access_token";


@Injectable({ providedIn: 'root' })
export class AuthService {

  loginDispatcher: EventEmitter<any> = new EventEmitter();

  constructor(private http: HttpClient, @Inject(AUTH_API_URL) private apiUrl: string, private jwtHelper: JwtHelperService, private router: Router) { }

  login(username: string, password: string): Observable<Token> {
    return this.http.post<Token>(`${this.apiUrl}api/auth/login`, { username, password })
      .pipe(tap(token => {
        localStorage.setItem(ACCESS_TOKEN_KEY, token.accessToken);
        this.loginDispatcher.emit();
      }));
  }

  isAuthenticated(): boolean {
    var token = localStorage.getItem(ACCESS_TOKEN_KEY);
    return token && !this.jwtHelper.isTokenExpired(token);
  }

  logout() {
    localStorage.removeItem(ACCESS_TOKEN_KEY);
    this.loginDispatcher.emit();
    this.router.navigate(['/login'], { queryParams: { returnUrl: this.router.url } });
  }

  getLoginDispatcher(): EventEmitter<any> {
    return this.loginDispatcher;
  }

  getUser(): User {
    var token = localStorage.getItem(ACCESS_TOKEN_KEY);
    if (token == null) return null;
    let userJson = this.jwtHelper.decodeToken(token);
    let user = new User();
    user.userName = userJson.sub;
    user.roles = userJson.role;
    return user;
  }
}
