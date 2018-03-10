import { Component } from '@angular/core';
import { NavController, AlertController, LoadingController, Loading } from 'ionic-angular';
import { Events } from 'ionic-angular';
import { Http } from '@angular/http';
import { HomePage } from '../home/home';

export class User {
    Id: string;
    Password: string;

    constructor(Id: string, Password: string) {
        this.Id = Id;
        this.Password = Password;
    }
}

@Component({
    selector: 'page-login',
    templateUrl: 'login.html',
})
export class LoginPage {
    loading: Loading;
    credentialId: string;
    credentialPwd: string;
    userResult: User;

    private address = "localhost";//use "169.254.80.80" for android emulator (run Visual Studio as admin), "localhost" for ripple or browser
    private port = "54610";

    constructor(public events: Events, private nav: NavController, private alertCtrl: AlertController, private loadingCtrl: LoadingController, private http: Http) { }

    /*public createAccount() {
        this.nav.push('RegisterPage');
    }*/

    //sends credentialId and credentialPwd to the server and checks if user can login
    login() {
        this.showLoading()
        this.http.get('http://' + this.address + ':' + this.port + '/Auth/login/' + this.credentialId + '/' + this.credentialPwd)
            .subscribe(res => {
                //if correct the DB user is returned
                var result = res.json();
                if (result.Id == this.credentialId) {
                    localStorage.setItem("userId", this.credentialId);
                    //tell app.component the login is successful
                    this.events.publish('login', Date.now());
                    //redirect to home page
                    this.nav.setRoot(HomePage);
                } else {
                    if (result.Id == "Username errato") {
                        this.showError("Username errato");
                    }
                    if (result.Id == "Password errata") {
                        this.showError("Password errata");
                    }
                }
            });
    }

    //show loadin
    showLoading() {
        this.loading = this.loadingCtrl.create({
            content: 'Accesso in corso...',
            dismissOnPageChange: true
        });
        this.loading.present();
    }

    //show error
    showError(text) {
        this.loading.dismiss();

        let alert = this.alertCtrl.create({
            title: 'Errore',
            subTitle: text,
            buttons: ['OK']
        });
        alert.present(prompt);
    }
}