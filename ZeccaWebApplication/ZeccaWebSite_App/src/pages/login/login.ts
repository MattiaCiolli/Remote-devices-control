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

    constructor(public events: Events, private nav: NavController, private alertCtrl: AlertController, private loadingCtrl: LoadingController, private http: Http) { }

    public createAccount() {
        this.nav.push('RegisterPage');
    }

    login() {
        this.showLoading()
        this.http.get('http://localhost:54610/Auth/login/' + this.credentialId + '/' + this.credentialPwd)
            .subscribe(res => {
                var result = res.json();
                if (result.Id == this.credentialId) {
                    localStorage.setItem("userId", this.credentialId);
                    this.events.publish('login', Date.now());
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

    showLoading() {
        this.loading = this.loadingCtrl.create({
            content: 'Accesso in corso...',
            dismissOnPageChange: true
        });
        this.loading.present();
    }

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