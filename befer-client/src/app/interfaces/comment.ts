import { IBase } from "./base";

export interface IComment {
    objectId?: string;
    createdAt?: string;
    updatedAt?: string;
    deletedOn?: string;
    isDeleted?: boolean;
    content: string;
    postId: string;
    author?: {
        objectId: string,
        fullName: string,
        username: string,
        email: string
    }
}