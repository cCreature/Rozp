using StatsBase
n = 5

processorsIds = sample(1:100, n, replace = false)
println("ID procesorów kolejno: $(processorsIds)")
channels = [Channel{Tuple{Int, Int}}(n) for i=1:n] #myId, message
global chosenLeader = 0
left = true

function doJob(id)
    global chosenLeader
    neighbour = left ? (id-1)%n == 0 ? n : (id-1)%n : (id+1)%n == 0 ? 1 : (id+1)%n

    put!(channels[neighbour], (id, processorsIds[id]))

    while true
        neigh, message = take!(channels[id])
        if(message == -1)
            put!(channels[neighbour], (id, -1))
            break
        end

        if(message > processorsIds[id])
            put!(channels[neighbour], (id, message))
        elseif (message < processorsIds[id])
        else
            chosenLeader = processorsIds[id]
            put!(channels[neighbour], (id, -1))
            break
        end
    end
end


function leaderElection()
    @sync for id = 1 : n
        @async doJob(id)
    end
    println("--------\n LIDER: $(chosenLeader)")

end

leaderElection()
