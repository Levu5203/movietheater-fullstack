import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { faCamera } from '@fortawesome/free-solid-svg-icons';
import { PromotionService } from '../../../services/promotion/promotion-service';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormsModule } from '@angular/forms';

@Component({
  selector: 'app-updatepromotion',
  standalone: true,
  imports: [FontAwesomeModule, CommonModule, FormsModule],
  templateUrl: './updatepromotion.component.html',
  styleUrls: ['./updatepromotion.component.css']
})
export class UpdatepromotionComponent implements OnInit {
  public faCamera = faCamera;
  public showErrorMessage: boolean = false;
  public errorMessage: string = '';
  
  promotion = {
    id: '',
    promotionTitle: '',
    description: '',
    discount: '',
    startDate: '',
    endDate: '',
    image: ''
  };

  selectedImage: File | null = null;
  previewImage: string | null = null;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private promotionService: PromotionService,
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.promotionService.getPromotionById(id).subscribe((data) => {
        this.promotion = data;
        this.promotion.startDate = data.startDate.split("T")[0]; // Chuyển về yyyy-MM-dd
        this.promotion.endDate = data.endDate.split("T")[0];
        this.previewImage = data.image; // Hiển thị ảnh cũ
        console.log(this.previewImage)
      });
    }
  }

  onFileSelected(event: any) {
    if (event.target.files.length > 0) {
      this.selectedImage = event.target.files[0] as File;

      // Xem trước ảnh mới
      const reader = new FileReader();
      reader.onload = (e: any) => {
        this.previewImage = e.target.result;
      };
      reader.readAsDataURL(this.selectedImage);
    }
  }

  updatePromotion() {
    // Validate promotion title and description
    if (!this.promotion.promotionTitle.trim() || !this.promotion.description.trim()) {
      this.showErrorMessage = true;
      this.errorMessage = 'Please fill in all required fields';
      return;
    }
  
    // Check if discount is a valid decimal number
    const discount = Number(this.promotion.discount);
    if (isNaN(discount) || discount <= 0 || !Number.isFinite(discount)) {
      this.showErrorMessage = true;
      this.errorMessage = 'Discount must be a positive decimal number';
      return;
    }
  
    // Check if dates are provided
    if (!this.promotion.startDate || !this.promotion.endDate) {
      this.showErrorMessage = true;
      this.errorMessage = 'Please select both start and end dates';
      return;
    }
  
    // Check if endDate is not before startDate
    const startDate = new Date(this.promotion.startDate);
    const endDate = new Date(this.promotion.endDate);
    if (endDate < startDate) {
      this.showErrorMessage = true;
      this.errorMessage = 'End date cannot be before start date';
      return;
    }
  
    // No need to check for image as it's optional during update
    // (we already have the existing image)

    const formData = new FormData();
    formData.append('id', this.promotion.id);
    formData.append('promotionTitle', this.promotion.promotionTitle);
    formData.append('description', this.promotion.description);
    formData.append('discount', this.promotion.discount);
    formData.append('startDate', this.promotion.startDate);
    formData.append('endDate', this.promotion.endDate);
    if (this.selectedImage) {
      formData.append('image', this.selectedImage);
    }
    console.log(typeof(this.selectedImage));

    this.promotionService.updatePromotion(this.promotion.id, formData).subscribe({
      next: () => {
        this.router.navigate(['admin/promotionmanagement']);
      },
      error: (err) => {
        console.error('Error updating promotion:', err);
        this.showErrorMessage = true;
        this.errorMessage = err.error?.message || 'Failed to update promotion. Please try again.';
      }
    });
  }

  cancelUpdate() {
    this.router.navigate(['admin/promotionmanagement']);
  }
}