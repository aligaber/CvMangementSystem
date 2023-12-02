import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CvListComponent } from './cv-list/cv-list.component';

const routes: Routes = [
  {
    path: 'cv-list',
    component: CvListComponent
  },
  { path: '', redirectTo: 'cv-list', pathMatch: 'full' },
  { path: '**', redirectTo: 'cv-list' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}

export const routedComponents = [CvListComponent];
