import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";

import { StudentListComponent } from "./students/student-list/student-list.component";
import { StudentDetailsComponent } from "./students/student-details/student-details.component";
import { StudentAddEditComponent } from "./students/student-add-edit/student-add-edit.component";

const routes: Routes = [
  { path: 'students/add', component: StudentAddEditComponent },
  { path: 'students/:id', component: StudentDetailsComponent },
  { path: 'students/edit/:id', component: StudentAddEditComponent },
  { path: '', component: StudentListComponent, pathMatch: 'full' },
  { path: '**', redirectTo: '/' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
