import { Component } from '@angular/core';
import { NavController, NavParams } from 'ionic-angular';
import { Http } from '@angular/http';

@Component({
    selector: 'page-home',
    templateUrl: 'home.html'
})
export class HomePage {

    public devices: any[];

    constructor(private http: Http) {
        this.http.get('http://localhost:54610/Devices')
            .subscribe(res => this.devices = res.json());
    }
}