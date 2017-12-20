import {Injectable} from '@angular/core';
import {Subject} from 'rxjs/Rx';
export class RequestHandlerService {
    show: Subject<boolean> = new Subject();
}