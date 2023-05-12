<?php
namespace App\Games;

use App\Games\Game;
use App\Games\HighScore;
use SilverStripe\Admin\ModelAdmin;

/**
 * Class \App\Database\ExperienceAdmin
 *
 */
class HighScoreAdmin extends ModelAdmin
{
    private static $managed_models = array (
        HighScore::class,
        Game::class,
    );

    private static $url_segment = "highscores-list";

    private static $menu_title = "Highscore";

    private static $menu_icon = "app/client/icons/block-text.svg";
}
