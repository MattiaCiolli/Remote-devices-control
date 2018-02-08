import { Component, ViewChild, ElementRef  } from '@angular/core';
import { NavController, NavParams, PopoverController, ViewController } from 'ionic-angular';
import { Http } from '@angular/http';


@Component({
    template: `
<h2 style="margin:0; padding:24px 24px 20px 24px; font-size: 22px;">Dispositivi:</h2>
<ion-searchbar [(ngModel)]="searchterm" (ionInput)="setFilteredItems()" style="border-top:solid; border-width:1px; border-color:#f1f1f1;"></ion-searchbar>
<ion-list radio-group [(ngModel)]="selected">
<ion-item *ngFor="let dev of devs">
<ion-radio (ionSelect)="close(dev.id)"></ion-radio>
<ion-label>{{dev.id}}</ion-label>
</ion-item>
</ion-list>
`
})

export class PopoverPage {
    searchterm: string = '';
    selected: string;
    background: string;
    //contentEle: any;
    //textEle: any;
    devsTot: any[];
    devs: any[];
    fontFamily;

    colors = {
        'white': {
            'bg': 'rgb(255, 255, 255)',
            'fg': 'rgb(0, 0, 0)'
        },
        'tan': {
            'bg': 'rgb(249, 241, 228)',
            'fg': 'rgb(0, 0, 0)'
        },
        'grey': {
            'bg': 'rgb(76, 75, 80)',
            'fg': 'rgb(255, 255, 255)'
        },
        'black': {
            'bg': 'rgb(0, 0, 0)',
            'fg': 'rgb(255, 255, 255)'
        },
    };

    constructor(private navParams: NavParams, public viewCtrl: ViewController) {
    }

    setFilteredItems() {

        this.devs = this.devsTot.filter((dev) => {
            return dev.id.toLowerCase().indexOf(this.searchterm.toLowerCase()) > -1;
        });    

    }

    close(selected) {
        this.viewCtrl.dismiss(selected);
    }

    ngOnInit() {
        if (this.navParams.data) {
            //this.contentEle = this.navParams.data.contentEle;
            //this.textEle = this.navParams.data.textEle;
            this.devsTot = this.navParams.data.devsTot;
            this.devs = this.devsTot;
            //this.background = this.getColorName(this.contentEle.style.backgroundColor);
            //this.setFontFamily();
        }
    }

    /*getColorName(background) {
        let colorName = 'white';

        if (!background) return 'white';

        for (var key in this.colors) {
            if (this.colors[key].bg == background) {
                colorName = key;
            }
        }

        return colorName;
    }
    
    setFontFamily() {
        if (this.textEle.style.fontFamily) {
            this.fontFamily = this.textEle.style.fontFamily.replace(/'/g, "");
        }
    }
    
    changeBackground(color) {
        this.background = color;
        this.contentEle.style.backgroundColor = this.colors[color].bg;
        this.textEle.style.color = this.colors[color].fg;
    }

    changeFontSize(direction) {
        this.textEle.style.fontSize = direction;
    }

    changeFontFamily() {
        if (this.fontFamily) this.textEle.style.fontFamily = this.fontFamily;
    }*/
}

@Component({
    selector: 'page-devSelector',
    templateUrl: 'devSelector.html'
})

export class DevSel {

    @ViewChild('popoverContent', { read: ElementRef }) content: ElementRef;
    @ViewChild('popoverText', { read: ElementRef }) text: ElementRef;
    public loading = false;
    public devices: any[];
    public functions: any[];
    public requestResults: any[];

    selectedDevice: string = "--- ";
    selectedFunctions: number[];

    deviceFunctions(selectedDev) {
        this.http.get('http://localhost:54610/Devices/' + selectedDev + '/Functions')
            .subscribe(res => this.functions = res.json());
    }

    requestInfosByFunctions() {
        this.requestResults = undefined;
        var fid = this.selectedFunctions.toString();
        fid = fid.replace(/,/g, '&');
        this.loading = true;
        this.http.get('http://localhost:54610/Devices/' + this.selectedDevice + '/RequestInfos/?' + fid)
            .subscribe(res => {
                this.requestResults = res.json();
                this.loading = false;
            });
    }

    presentPopover(ev?: Event) {

        let popover = this.popoverCtrl.create(PopoverPage, {
            //contentEle: this.content.nativeElement,
            //textEle: this.text.nativeElement,
            devsTot: this.devices
        });
        let ev1 = {
            target: {
                getBoundingClientRect: () => {
                    return {
                        top: '100'
                    };
                }
            }
        };
        popover.present({ ev: ev1, animate:false });
        popover.onDidDismiss(data => { this.selectedDevice = data; this.deviceFunctions(this.selectedDevice);});
    }

    constructor(private http: Http, private popoverCtrl: PopoverController) {
        this.http.get('http://localhost:54610/Devices')
            .subscribe(res => this.devices = res.json());
    }
}