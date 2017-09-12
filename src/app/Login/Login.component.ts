
import {Component} from "@angular/core";
import {LoginModel} from "./LoginModel";
import {Router} from "@angular/router";
import { Http } from '@angular/http';

@Component({
  templateUrl: './Login.component.html'
})
export class LoginComponent {
  constructor(private router: Router, private http: Http){}


  model = new LoginModel('', '');

  errorMessage = '';

  submitted = false;

  onSubmit() {
    this.submitted = true;
    console.log(this.model.username);
    this.http.post('/api/auth/login', {
      username: this.model.username,
      password: this.model.password
    }).subscribe((resp) => {
      const response: {jwt: string} = resp.json();
      localStorage.setItem('jwt', response.jwt);
      this.errorMessage = "Login success! Redirecting...";
      setTimeout(() => this.router.navigateByUrl('/twilioApp'), 100)
    }, (error) => {this.errorMessage = error})
  }

}
