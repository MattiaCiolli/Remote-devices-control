import { Component } from '@angular/core';
import { NavController, AlertController, LoadingController, Loading } from 'ionic-angular';
//import { AuthService } from '../../providers/auth-service';
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

    constructor(private nav: NavController, private alertCtrl: AlertController, private loadingCtrl: LoadingController, private http: Http) { }

    public createAccount() {
        this.nav.push('RegisterPage');
    }

    login() {
        this.showLoading()
        /*this.auth.login(this.credentialId, this.credentialPwd).then(data => {

            this.userResult = data;
            alert("A"+data);
            if (this.userResult.Id == this.credentialId && this.userResult.Password == this.credentialPwd) {
                this.nav.setRoot('HomePage');
            } else {
                this.showError("Access Denied");
            }
        });
    }*/
        this.http.get('http://localhost:54610/Auth/login/' + this.credentialId + '/' + this.credentialPwd)
            .subscribe(res => {
                var result = res.json();
                if (result.Id == this.credentialId) {
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
            content: 'Please wait...',
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