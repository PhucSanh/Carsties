'use client'

import Countdown, { zeroPad } from "react-countdown"

const render = (({ days, hours, minutes, seconds, completed }: { days: number, hours: number, minutes: number, seconds: number, completed: boolean }) => {

    return (
        <div className={`border-2 border-white text-white px-2 py-1 rounded-lg
            flex justify-center ${completed ? 'bg-gray-500' :
                (days === 0 && hours < 10) ? 'bg-amber-600' : 'bg-green-600'}
            `}
        >
            {completed ? <span className="text-lg font-semibold">Auction Finished</span> :
                <span suppressHydrationWarning className="text-lg font-semibold">{days}:{zeroPad(hours)}:{zeroPad(minutes)}:{zeroPad(seconds)}</span>}
        </div>
    )


})
type Props = {
    auctionEndDate: string
}
export default function CountdownTimer({ auctionEndDate }: Props) {
    return (
        <Countdown date={auctionEndDate} renderer={render} />
    )
}
