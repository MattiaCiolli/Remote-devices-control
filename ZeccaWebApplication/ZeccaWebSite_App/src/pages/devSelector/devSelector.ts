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

    //filters devices in list on input in searchbar
    setFilteredItems() {

        this.devs = this.devsTot.filter((dev) => {
            return dev.id.toLowerCase().indexOf(this.searchterm.toLowerCase()) > -1;
        });    

    }

    //closes popover and passes the selected device to the devSelector's controller
    close(selected) {
        this.viewCtrl.dismiss(selected);
    }

    //initializes popover with all devices
    ngOnInit() {
        if (this.navParams.data) {
            this.devsTot = this.navParams.data.devsTot;
            this.devs = this.devsTot;
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
    private address = "localhost";//use "169.254.80.80" for android emulator (run Visual Studio as admin), "localhost" for ripple or browser
    private port = "54610";

    selectedDevice: string = "--- ";
    selectedFunctions: number[];

    //gets device functions
    deviceFunctions(selectedDev) {
        //subscribe executes the code in brackets only when data is received
        this.http.get('http://' + this.address + ':' + this.port + '/Devices/' + selectedDev + '/Functions')
            .subscribe(res => this.functions = res.json());
    }

    //gets infos for each selected function
    requestInfosByFunctions() {
        this.requestResults = undefined;
        var fid = this.selectedFunctions.toString();
        fid = fid.replace(/,/g, '&');
        this.loading = true;
        this.http.get('http://' + this.address + ':' + this.port + '/Devices/' + this.selectedDevice + '/RequestInfos/?' + fid)
            .subscribe(res => {
                this.requestResults = res.json();
                this.loading = false;
            });
    }

    //show select device popover
    presentPopover(ev?: Event) {

        let popover = this.popoverCtrl.create(PopoverPage, {
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
        popover.present({ ev: ev1, animate: false });
        //when dismissed get the selected device and launch deviceFunctions function
        popover.onDidDismiss(data => { this.selectedDevice = data; this.deviceFunctions(this.selectedDevice);});
    }

    //load device list on startup
    constructor(private http: Http, private popoverCtrl: PopoverController) {
        this.http.get('http://' + this.address + ':' + this.port + '/Devices')
            .subscribe(res => this.devices = res.json());
    }
}