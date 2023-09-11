import { Guid } from "guid-typescript";
import { Category } from "./Category";
import { Image } from "./Image";

export interface Product {
    id: Guid;
    title: string;
    description: string;
    price: number;
    category: Category;
    inventory: number;
    image: Image
}

export interface ProductCreateDto {
    title: string;
    description: string;
    price: number;
    category: Category;
    inventory: number;
    image: Image
}