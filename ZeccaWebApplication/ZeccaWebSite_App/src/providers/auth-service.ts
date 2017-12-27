import { Injectable } from '@angular/core';
import {Observable} from 'rxjs/Observable';
//needed for http
//import 'npm:rxjs@5.2.0/add/operator/map'; 
import { Http } from '@angular/http';

export class User {
    Id: string;
    Password: string;

    constructor(Id: string, Password: string) {
        this.Id = Id;
        this.Password = Password;
    }
}

@Injectable()
export class AuthService {
    currentUser: User;
    tempUser: User;

    constructor(private http: Http) { }

    public login(id_in, pwd_in):any {

       /* if (id_in === null || pwd_in === null) {
            this.tempUser.Id = "Inserire dati di accesso"
            return JSON.stringify(this.tempUser);
        } else {

            this.http.get('http://localhost:54610/Auth/login/' + id_in + '/' + pwd_in).map((response) => {

                let data = response.json(); alert(this.tempUser);

                /*if (data) {

                    this.tempUser = data;

                    if (this.tempUser.Id == id_in && this.tempUser.Password == pwd_in) {

                        this.currentUser.Id = this.tempUser.Id;
                        this.currentUser.Password = this.tempUser.Password;
                        return JSON.stringify(this.currentUser);
                    } else {
                        if (this.tempUser.Id == "Username errato") {
                            return JSON.stringify(this.tempUser);
                        }
                        if (this.tempUser.Id == "Password errata") {
                            return JSON.stringify(this.tempUser);
                        }
                    }
                }
            });
        }*/
    }

            /*this.http.get('http://localhost:54610/Auth/login/' + id_in + '/' + pwd_in).map((response) => {

                let data
                .subscribe(res => {
                this.tempUser = res.json(); alert(this.tempUser);

                    if (this.tempUser.Id == id_in && this.tempUser.Password == pwd_in) {

                        this.currentUser.Id = this.tempUser.Id;
                        this.currentUser.Password = this.tempUser.Password;
                        return this.currentUser;
                    } else {
                        if (this.tempUser.Id == "Username errato") {
                            return this.tempUser;
                        }
                        if (this.tempUser.Id == "Password errata") {
                            return this.tempUser;
                        }
                    }
                });
        }
    }*/

    public register(credentials) {
        if (credentials.email === null || credentials.password === null) {
            return Observable.throw("Please insert credentials");
        } else {
            // At this point store the credentials to your backend!
            return Observable.create(observer => {
                observer.next(true);
                observer.complete();
            });
        }
    }

    public getUserInfo(): User {
        return this.currentUser;
    }

    public logout() {
        return Observable.create(observer => {
            this.currentUser = null;
            observer.next(true);
            observer.complete();
        });
    }
}