import { Component } from '@angular/core';
import { MenuController, App } from 'ionic-angular';
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
}

@Component({
    selector: 'page-home',
    templateUrl: 'home.html'
})
export class HomePage {

    public countDev = 1;
    public hideReq: boolean[];
    private effectiveCount = 0;

    addDevice() {
        this.countDev++;
        this.hideReq[this.countDev - 1] = false;
    }

    orderValue() {
        var elemList = document.getElementsByClassName("reqOrder");
        //this.effectiveCount++;
        //alert(elemList.length);
        for (var i = 0; i < elemList.length; i++) {            
            elemList[i].innerHTML = "Richiesta " + (i + 1);
            i++;
        }
    }

    getNumber() {
        return new Array(this.countDev);
    }

    deleteRequest(i) {
        /*     
        var delDiv = document.getElementById(i.toString());
        var parentDiv = delDiv.parentNode;
        parentDiv.removeChild(delDiv);*/
        this.hideReq[i] = true;
        this.orderValue();
    }

    constructor() {
        this.hideReq = new Array(100);
        this.hideReq[0] = false;
    }
}