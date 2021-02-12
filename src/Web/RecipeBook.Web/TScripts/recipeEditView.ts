$(document).ready(function () {
    let fTasteStarsVote = new fiveStarsVote("TasteRateStars");
    fTasteStarsVote.startListenToVote();
    let fEasyStarsVote = new fiveStarsVote("EasyRateStars");
    fEasyStarsVote.startListenToVote();
});