import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CategoryTaskDetailsComponent } from './category-task-details.component';

describe('CategoryTaskDetailsComponent', () => {
  let component: CategoryTaskDetailsComponent;
  let fixture: ComponentFixture<CategoryTaskDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CategoryTaskDetailsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CategoryTaskDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
