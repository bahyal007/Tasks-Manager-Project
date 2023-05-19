import { Component, OnInit } from '@angular/core';
import { Category } from './category';
import { HttpClient } from '@angular/common/http';
import { FormGroup, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { environment } from '../environment/environment';

@Component({
  selector: 'app-category-edit',
  templateUrl: './category-edit.component.html',
  styleUrls: ['./category-edit.component.css']
})
export class CategoryEditComponent implements OnInit {
  category?: Category;
  form!: FormGroup;
  id!: number;

  constructor(private http: HttpClient, private activatedRoute: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {
    this.form = new FormGroup(
      {
        name: new FormControl(''),
      }
    );

    this.loadData();
  }

  loadData(): void {
    let idParam = this.activatedRoute.snapshot.paramMap.get('id');
    let url = environment.baseUrl + `api/TaskCategories/${idParam}`;
    this.http.get<Category>(url).subscribe(result => {
      this.category = result;
      this.form.patchValue(this.category);
    });
  }

  onSubmit() {
    let category = this.category!;

    category.name = this.form.controls['name'].value;
    
    let url = environment.baseUrl + `api/TaskCategories/${category.id}`;

    this.http.put<Category>(url, category).subscribe({
      next: () => {
        console.log(`Category ${category.id} was updated successfully`);
        this.router.navigate(['/categories']);
      }
    });
  }

}
