'use client';
import Image from "next/image";
import { useState } from "react";

type Props = {
    image: string
}


export default function CarImage({ image }: Props) {
    const [loading, setLoading] = useState(true);

    return (
        <Image src={image} alt="Car" fill

            priority
            sizes="(max-width:768px) 100vw,(max-width:1200px) 50vw,25vw"
            onLoad={() => setLoading(false)}
            className={`object-cover ease-in-out duration-700 ${loading ? 'opacity-0 scale-110' : 'opacity-100 scale-110'}`}

        />
    )
}
