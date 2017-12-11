import { Component } from '@angular/core';
import { NavController, NavParams } from 'ionic-angular';
import { Http } from '@angular/http';

@Component({
    selector: 'page-home',
    templateUrl: 'home.html'
})
export class HomePage {

    public devices: any[];
    public functions: any[];
    public requestResults: any[];

    selectedDevice: string;
    selectedFunctions: number[];

    deviceFunctions(selectedDev) {
        this.http.get('http://localhost:54610/Devices/' + selectedDev +'/Functions')
            .subscribe(res => this.functions = res.json());
    }

    requestInfosByFunctions() {
        var fid = this.selectedFunctions.toString();
        fid = fid.replace(/,/g, '&');
        this.http.get('http://localhost:54610/Devices/' + this.selectedDevice + '/RequestInfos/?' + fid)
            .subscribe(res => this.requestResults = res.json());
    }

    constructor(private http: Http) {
        this.http.get('http://localhost:54610/Devices')
            .subscribe(res => this.devices = res.json());
    }
}