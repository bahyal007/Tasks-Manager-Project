import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CategoriesComponent } from './categories/categories.component';
//import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { TasksComponent } from './tasks/tasks.component';
import { LoginComponent } from './auth/login.component';
import { CategoryTaskDetailsComponent } from './categories/category-task-details.component';
import { CategoryEditComponent } from './categories/category-edit.component';
import { RoleGuard } from './role-guard.guard';

const routes: Routes = [
  { path: 'tasks', component: TasksComponent, pathMatch: 'full' },
  { path: 'categories', component: CategoriesComponent },
  { path: 'categorytaskdetails/:id', component: CategoryTaskDetailsComponent },
  { path: 'categoryedit/:id', component: CategoryEditComponent, canActivate: [RoleGuard], data:{expectedRoles: ['Administrator']} },
  { path: 'login', component: LoginComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }