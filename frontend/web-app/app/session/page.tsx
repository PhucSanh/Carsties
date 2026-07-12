import { auth } from '@/auth'
import React from 'react'

export default async function Session() {
    const session = await auth();
    return (
        <div>
            <h1>Session</h1>
            <div className='bg-blue-200 border-2 border-blue-500'>
                <h3 className='text-lg font-bold'>Session Information</h3>
                <pre className='whitespace-pre-wrap break-all'>{JSON.stringify(session, null, 2)}</pre>
            </div>
        </div>
    )
}
