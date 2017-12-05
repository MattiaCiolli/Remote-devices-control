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
    public selectedDevice: string;
    public selectedFunctions: any[];

    deviceFunctions(selectedDev) {
        this.selectedDevice = selectedDev;
        this.http.get('http://localhost:54610/Devices/' + selectedDev +'/Functions')
            .subscribe(res => this.functions = res.json());
    }

    setFunctionsId(selectedFun) {
        this.selectedFunctions = selectedFun;
    }

    requestInfosByFunctions(selectedDevice, selectedFunctions) {
        this.http.get('http://localhost:54610/Devices/' + selectedDevice + '/RequestInfos/' + encodeURIComponent(JSON.stringify({ "idf": selectedFunctions })))
            .subscribe(res => this.functions = res.json());
    }

    constructor(private http: Http) {
        this.http.get('http://localhost:54610/Devices')
            .subscribe(res => this.devices = res.json());
    }
}