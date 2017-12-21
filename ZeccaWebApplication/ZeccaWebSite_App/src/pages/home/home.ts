import { Component } from '@angular/core';
import { NavController, NavParams, MenuController, App } from 'ionic-angular';
import { Http } from '@angular/http';
import { DevSel } from '../devSelector/devSelector';

@Component({
    template: `
<ion-header>
    <ion-navbar>
        <button ion-button menuToggle icon-only>
            <ion-icon name='menu'></ion-icon>
        </button>
        <ion-title>
            <ion-grid>
                <ion-row>
                    <ion-col col-4 offset-4>
                        <img src="img/zecca.png" id="logo" />
                    </ion-col>
                </ion-row>
            </ion-grid>
        </ion-title>
    </ion-navbar>
</ion-header>
`
})
export class BasicPage {
    constructor(app: App, menu: MenuController) {
        menu.enable(true);
    }

    openAddPage() {
        //this.nav.push(AddPage)
    }
}

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