import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { ListComponent } from './list/list.component';

export const routes: Routes = [
  {path:"", redirectTo:"home", pathMatch:'full'},
  {path:"home", component: AppComponent},
  {path:"list", component: ListComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
