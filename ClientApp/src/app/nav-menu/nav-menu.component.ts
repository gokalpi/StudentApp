import { Component } from "@angular/core";
import { Observable } from "rxjs";
import { AuthorizeService } from "src/api-authorization/authorize.service";
import { TranslateService } from "@ngx-translate/core";
import defaultLanguage from "../../assets/i18n/en.json";

@Component({
  selector: "app-nav-menu",
  templateUrl: "./nav-menu.component.html",
  styleUrls: ["./nav-menu.component.css"]
})
export class NavMenuComponent {
  isExpanded = false;
  isAuthenticated: Observable<boolean>;

  public activeLanguage = "en";

  constructor(
    private authorizeService: AuthorizeService,
    public translate: TranslateService
  ) {
    translate.addLangs(["en", "fr", "tr"]);
    translate.setTranslation('en', defaultLanguage);
    translate.setDefaultLang('en');
  }

  ngOnInit() {
    this.isAuthenticated = this.authorizeService.isAuthenticated();

    if (sessionStorage.getItem("language")) {
      this.switchLanguage(sessionStorage.getItem("language"));
    } else {
      this.switchLanguage(this.activeLanguage);
    }
  }

  switchLanguage(language: string) {
    sessionStorage.setItem("language", language);
    this.activeLanguage = language;
    this.translate.use(language);
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
