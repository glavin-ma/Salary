import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { JwtModule } from '@auth0/angular-jwt';

import { AppComponent } from "./app.component";
import { routs, toastrSettings, jwtSettings, AUTH_API_URL, MaterialModule  } from "."
import { NavMenuComponent, LoginComponent, LoginMenuComponent, MyInfoComponent, SalariesComponent  } from "./components"
import { AuthInterceptor, ErrorInterceptor  } from "./classes"
import { environment } from "../environments/environment";
import { DateFormatPipe } from "./pipes/date-format-pipe";


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    LoginComponent,
    LoginMenuComponent,
    MyInfoComponent,
    SalariesComponent

  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    RouterModule.forRoot(routs),
    ToastrModule.forRoot(toastrSettings),
    JwtModule.forRoot(jwtSettings),
    MaterialModule
  ],
  providers: [
    { provide: AUTH_API_URL, useValue: environment.authApi },
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    DateFormatPipe
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
