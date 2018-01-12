import { Component, Inject, ViewChild } from '@angular/core';
import { NavController, NavParams, MenuController, App, Platform } from 'ionic-angular';
import { StatusBar, Splashscreen } from 'ionic-native';
import { Events } from 'ionic-angular';

import { HomePage } from '../pages/home/home';
import { DevSel } from '../pages/devSelector/devSelector';
import { LoginPage } from '../pages/login/login';

@Component({
    templateUrl: 'app.html'
})
export class MyApp {

    storedUserId: string;

    @ViewChild('content') nav: NavController;
    rootPage = LoginPage;
    constructor( @Inject(Platform) platform, public events: Events) {
        platform.ready().then(() => {
            // the platform is ready and our plugins are available.
            // here you can do any higher level native things you might need.
            StatusBar.styleDefault();
            Splashscreen.hide();
            events.subscribe('login', (time) => {
                // time is the same argument passed in `events.publish(login, time)`
                this.storedUserId = localStorage.getItem("userId");
            });

        });
    }

    logout() {
        localStorage.removeItem("userId");
        this.nav.setRoot(LoginPage);
    }

    openPage(page: any) {
        this.nav.setRoot(page);
    }

}
