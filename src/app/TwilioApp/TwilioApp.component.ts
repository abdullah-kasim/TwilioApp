
import {Component} from "@angular/core";
import {Router} from "@angular/router";
import {Headers, Http} from '@angular/http';
import {TwilioAppModel} from "./TwilioAppModel";

@Component({
  templateUrl: './TwilioApp.component.html'
})
export class TwilioAppComponent {
  constructor(private router: Router, private http: Http){}


  model = new TwilioAppModel('', '');

  errorMessage = '';

  submitted = false;

  onSubmit() {
    this.submitted = true;
    this.errorMessage = "Submitting....";
    const headers = new Headers();
    headers.append('Authorization', `Bearer ${localStorage.getItem('jwt')}`);
    this.http.post('/api/twilio/main/sendSms', {
      message: this.model.message,
      phoneNumber: this.model.phoneNumber
    }, {headers}).subscribe((resp) => {
      const response: {jwt: string} = resp.json();
      localStorage.setItem('jwt', response.jwt);
      this.errorMessage = "Message submission successful!";
      setTimeout(() => this.router.navigateByUrl('/twilioApp'), 100)
    }, (error) => {this.errorMessage = error})
  }

}
