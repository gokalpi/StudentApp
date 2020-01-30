import { BrowserModule } from "@angular/platform-browser";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { ToastrModule } from "ngx-toastr";
import { NgModule } from "@angular/core";
import { ReactiveFormsModule, FormsModule } from "@angular/forms";
import {
  HttpClientModule,
  HTTP_INTERCEPTORS,
  HttpClient
} from "@angular/common/http";
import { TranslateModule, TranslateLoader } from "@ngx-translate/core";
import { TranslateHttpLoader } from "@ngx-translate/http-loader";

import { AppComponent } from "./app.component";
import { AppRoutingModule } from "./app-routing.module";
import { ApiAuthorizationModule } from "src/api-authorization/api-authorization.module";
import { AuthorizeInterceptor } from "src/api-authorization/authorize.interceptor";

import { HomeComponent } from "./home/home.component";
import { NavMenuComponent } from "./nav-menu/nav-menu.component";
import { StudentListComponent } from "./students/student-list/student-list.component";
import { StudentDetailsComponent } from "./students/student-details/student-details.component";
import { StudentAddEditComponent } from "./students/student-add-edit/student-add-edit.component";

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    StudentListComponent,
    StudentAddEditComponent,
    StudentDetailsComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: "ng-cli-universal" }),
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
    HttpClientModule,
    ApiAuthorizationModule,
    ReactiveFormsModule,
    FormsModule,
    AppRoutingModule,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient]
      }
    })
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}

// required for AOT compilation
export function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http);
}
