import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { RouterModule, Routes } from '@angular/router';
import {LoginComponent} from "./Login/Login.component";
import { FormsModule }   from '@angular/forms';
import {FrontpageComponent} from "./Frontpage/Frontpage.component";
import {HttpModule} from "@angular/http";
import {TwilioAppComponent} from "./TwilioApp/TwilioApp.component";

const appRoutes: Routes = [
  { path: 'login', component: LoginComponent },
  {path: "", component: FrontpageComponent},
  {path: 'twilioApp', component: TwilioAppComponent}
];

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    FrontpageComponent,
    TwilioAppComponent
  ],
  imports: [
    BrowserModule,
    RouterModule.forRoot(appRoutes,  { useHash : true}),
    FormsModule,
    HttpModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
