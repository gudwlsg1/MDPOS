using MDPOS.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDPOS.ViewModel
{

    public partial class FoodViewModel
    {
        public ObservableCollection<Food> Items { get; set; }

        public FoodViewModel()
        {
            Items = new ObservableCollection<Food>();
            StatItems = new ObservableCollection<Food>();
            InitData();
        }
        private void InitData()
        {
            // 김밥류
            Items.Add(new Food() { Category = Category.김밥류, Name = "돈까스김밥", Price = 3000, Orders = 0 });
            Items.Add(new Food() { Category = Category.김밥류, Name = "떡갈비김밥", Price = 3000, Orders = 0 });
            Items.Add(new Food() { Category = Category.김밥류, Name = "새우튀김김밥", Price = 4000, Orders = 0 });
            Items.Add(new Food() { Category = Category.김밥류, Name = "치즈김밥", Price = 2500, Orders = 0 });
            Items.Add(new Food() { Category = Category.김밥류, Name = "참치김밥", Price = 2500, Orders = 0 });
            Items.Add(new Food() { Category = Category.김밥류, Name = "킹소세지김밥", Price = 4000, Orders = 0 });
            Items.Add(new Food() { Category = Category.김밥류, Name = "소불고기김밥", Price = 4500, Orders = 0 });
            
            //식사류
            Items.Add(new Food() { Category = Category.식사류, Name = "호박죽", Price = 3500, Orders = 0 });
            Items.Add(new Food() { Category = Category.식사류, Name = "쇠고기야채죽", Price = 3500, Orders = 0 });
            Items.Add(new Food() { Category = Category.식사류, Name = "가츠돈", Price = 5000, Orders = 0 });
            Items.Add(new Food() { Category = Category.식사류, Name = "돈까스 김치볶음밥", Price = 6000, Orders = 0 });
            Items.Add(new Food() { Category = Category.식사류, Name = "치즈돈까스", Price = 6500, Orders = 0 });
            Items.Add(new Food() { Category = Category.식사류, Name = "오므라이스", Price = 6000, Orders = 0 });
            Items.Add(new Food() { Category = Category.식사류, Name = "김치볶음밥", Price = 5500, Orders = 0 });
            Items.Add(new Food() { Category = Category.식사류, Name = "황태해장국", Price = 6000, Orders = 0 });
            Items.Add(new Food() { Category = Category.식사류, Name = "순두부찌개", Price = 6000, Orders = 0 });
            Items.Add(new Food() { Category = Category.식사류, Name = "된장찌개", Price = 6000, Orders = 0 });
            Items.Add(new Food() { Category = Category.식사류, Name = "김치찌개", Price = 6000, Orders = 0 });
            Items.Add(new Food() { Category = Category.식사류, Name = "부대찌개", Price = 6000, Orders = 0 });
            Items.Add(new Food() { Category = Category.식사류, Name = "육개장", Price = 6000, Orders = 0 });
            Items.Add(new Food() { Category = Category.식사류, Name = "뚝배기불고기", Price = 6500, Orders = 0 });
            Items.Add(new Food() { Category = Category.식사류, Name = "직화낙지덮밥", Price = 7000, Orders = 0 });
            Items.Add(new Food() { Category = Category.식사류, Name = "직화소고기덮밥", Price = 7000, Orders = 0 });
            Items.Add(new Food() { Category = Category.식사류, Name = "직화제육덮밥", Price = 7000, Orders = 0 });
            Items.Add(new Food() { Category = Category.식사류, Name = "직화닭가슴살덮밥", Price = 7000, Orders = 0 });

            // 분식류
            Items.Add(new Food() { Category = Category.분식류, Name = "유부우동", Price = 5000, Orders = 0 });
            Items.Add(new Food() { Category = Category.분식류, Name = "한우사골 떡만두국", Price = 6000, Orders = 0 });
            Items.Add(new Food() { Category = Category.분식류, Name = "갈비만두", Price = 4000, Orders = 0 });
            Items.Add(new Food() { Category = Category.분식류, Name = "왕만두", Price = 4500, Orders = 0 });
            Items.Add(new Food() { Category = Category.분식류, Name = "치즈라볶이", Price = 5000, Orders = 0 });
            Items.Add(new Food() { Category = Category.분식류, Name = "라볶이", Price = 4000, Orders = 0 });

            // 계절음식
            Items.Add(new Food() { Category = Category.계절음식, Name = "냉면", Price = 6000, Orders = 0 });
            Items.Add(new Food() { Category = Category.계절음식, Name = "냉모밀", Price = 6000, Orders = 0 });

            //음료
            Items.Add(new Food() { Category = Category.음료, Name = "코카콜라", Price = 1000, Orders = 0, Barcode = "8801094017200" });
            //Items.Add(new Food() { Category = Category.음료, Name = "코카콜라", Price = 1000, Orders = 0, Barcode = "8801056055202" });
            Items.Add(new Food() { Category = Category.음료, Name = "펩시", Price = 1000, Orders = 0, Barcode = "8801056070809" });
        }

        internal List<Food> GetCategoryFood(string category)
        {
            return Items.Where(w => w.Category.ToString() == category).ToList();
        }

        internal Food GetFood(string barCode)
        {
            Food food = Items.Where(w => w.Barcode != null && w.Barcode.Equals(barCode)).FirstOrDefault();
            return food;
        }
    }
}
