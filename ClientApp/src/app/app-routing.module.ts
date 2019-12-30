import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";

import { AuthorizeGuard } from "src/api-authorization/authorize.guard";
import { StudentListComponent } from "./students/student-list/student-list.component";
import { StudentDetailsComponent } from "./students/student-details/student-details.component";
import { StudentAddEditComponent } from "./students/student-add-edit/student-add-edit.component";

const routes: Routes = [
  { path: 'students/add', component: StudentAddEditComponent, canActivate: [AuthorizeGuard] },
  { path: 'students/:id', component: StudentDetailsComponent, canActivate: [AuthorizeGuard] },
  { path: 'students/edit/:id', component: StudentAddEditComponent, canActivate: [AuthorizeGuard] },
  { path: '', component: StudentListComponent, pathMatch: 'full', canActivate: [AuthorizeGuard] },
  { path: '**', redirectTo: '/' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
