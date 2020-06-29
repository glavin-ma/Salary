import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { JwtModule } from '@auth0/angular-jwt';

import { AppComponent } from "./app.component";
import { routs, toastrSettings, jwtSettings, AUTH_API_URL  } from "."
import { HomeComponent, CounterComponent, FetchDataComponent, NavMenuComponent, LoginComponent, LoginMenuComponent  } from "./components"
import { AuthInterceptor, ErrorInterceptor  } from "./classes"
import { environment } from "../environments/environment";


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    LoginComponent,
    LoginMenuComponent 

  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    RouterModule.forRoot(routs),
    ToastrModule.forRoot(toastrSettings),
    JwtModule.forRoot(jwtSettings)
  ],
  providers: [
    { provide: AUTH_API_URL, useValue: environment.authApi },
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
