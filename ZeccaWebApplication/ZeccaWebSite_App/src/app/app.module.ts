import { AuthService } from './../providers/auth-service';
import { NgModule, ErrorHandler } from '@angular/core';
import { IonicApp, IonicModule, IonicErrorHandler } from 'ionic-angular';
import { MyApp } from './app.component';
import { HomePage } from '../pages/home/home';
import { DevSel } from '../pages/devSelector/devSelector';
import { PopoverPage } from '../pages/devSelector/devSelector';
import { LoginPage } from '../pages/login/login';

@NgModule({
  declarations: [
    MyApp,
      HomePage,
      DevSel,
      LoginPage,
      PopoverPage
  ],
  imports: [
    IonicModule.forRoot(MyApp)
  ],
  bootstrap: [IonicApp],
  entryComponents: [
    MyApp,
      HomePage,
      DevSel,
      LoginPage,
      PopoverPage
  ],
  providers: [
      { provide: ErrorHandler, useClass: IonicErrorHandler },
      AuthService
  ]
})
export class AppModule {}
