import { Component, OnInit } from '@angular/core';
import { CategoryTaskDetails } from './category-task-details';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { environment } from '../environment/environment';

@Component({
  selector: 'app-category-task-details',
  templateUrl: './category-task-details.component.html',
  styleUrls: ['./category-task-details.component.css']
})
export class CategoryTaskDetailsComponent implements OnInit {
  categoryTaskDetails?: CategoryTaskDetails
  constructor(private http: HttpClient, private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    let idParam = this.activatedRoute.snapshot.paramMap.get('id');
    let url = environment.baseUrl + `api/TaskCategories/CategoryTaskDetails/${idParam}`;
    this.http.get<CategoryTaskDetails>(url).subscribe(result => {
      this.categoryTaskDetails = result;
    });
  }
}
