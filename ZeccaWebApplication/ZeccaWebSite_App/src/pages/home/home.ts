import { Component } from '@angular/core';
import { NavController, NavParams } from 'ionic-angular';
import { Http } from '@angular/http';
import { DevSel } from '../devSelector/devSelector';

@Component({
    selector: 'page-home',
    templateUrl: 'home.html'
})
export class HomePage {

    public countDev=1;

    addDevice() {
        this.countDev++;
    }

    getNumber () {
        return new Array(this.countDev);
    }

    deleteRequest(i) {
       
        //var delBtn = document.getElementById("buttondel" + i.toString());
        var delDiv = document.getElementById(i.toString());
        var parentDiv = delDiv.parentNode;
        parentDiv.removeChild(delDiv);
        //this.countDev--;
    }

    constructor() {}
}